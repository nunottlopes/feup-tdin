const amqp = require("amqplib");
const _ = require("lodash");

const rabbitmqUrl = require("../config/config").rabbitmq.url;

let instance;

class AmqpManager {

    constructor() {
        this.queues = {}
    }

    async init() {
        this.connection = await amqp.connect(rabbitmqUrl);
        console.log("Sucessfully connected to AMQP: " + rabbitmqUrl);

        this.channel = await this.connection.createChannel();
        console.log("AMQP channel established");

        return this;
    }

    async send(queue, msg) {
        if (!this.connection) {
            await this.init();
        }
        await this.channel.assertQueue(queue, { durable: true });
        this.channel.sendToQueue(queue, msg)
    }

    async subscribe(queue, handler) {
        if (!this.connection) {
            await this.init();
        }
        if (this.queues[queue]) {
            const existingHandler = _.find(this.queues[queue], h => h === handler);
            if (existingHandler) {
                return () => this.unsubscribe(queue, existingHandler);
            }
            this.queues[queue].push(handler);
            return () => this.unsubscribe(queue, handler);
        }

        await this.channel.assertQueue(queue, { durable: true });
        this.queues[queue] = [handler];
        this.channel.consume(
            queue,
            async (msg) => {
                const ack = _.once(() => this.channel.ack(msg))
                this.queues[queue].forEach(h => h(msg, ack))
            }
        );
        return () => this.unsubscribe(queue, handler);
    }

    async unsubscribe(queue, handler) {
        _.pull(this.queues[queue], handler);
    }
}

AmqpManager.getInstance = async () => {
    if (!instance) {
        const manager = new AmqpManager();
        instance = manager.init();
    }
    return instance;
};

module.exports = AmqpManager;
