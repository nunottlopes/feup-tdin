require("dotenv").config();

module.exports = {
    mongodb: {
        url: `mongodb://${process.env.MONGODB_ADDRESS}:${process.env.MONGODB_PORT}/${process.env.MONGODB_DATABASE_NAME}`
    },
    rabbitmq: {
        url: `amqp://${process.env.RABBITMQ_ADDRESS}`
    }
};