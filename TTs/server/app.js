const express = require("express");
var path = require("path");
var cors = require("cors");
var mongoose = require("mongoose");
const mongoUrl = require("./config/config").mongodb.url;

var amqp = require('amqplib/callback_api');
amqp.connect('amqp://rabbitmq', function (error0, connection) {
    if (error0) {
        throw error0;
    }
    console.log('Succeeded connected to: amqp://rabbitmq:5672');
    connection.createChannel(function (error1, channel) {
        if (error1) {
            throw error1;
        }
        var queue = 'hello';
        var msg = 'Hello world';

        channel.assertQueue(queue, {
            durable: false
        });

        channel.sendToQueue(queue, Buffer.from(msg));
        console.log(" [x] Sent %s", msg);
    });
});

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(
    cors({
        origin: function (origin, callback) {
            callback(null, true);
        }
    })
);

const indexRouter = require("./routes/index");
const ticketRouter = require("./routes/ticket");
const secondaryRouter = require("./routes/secondary");

app.use("/api/", indexRouter);
app.use("/api/ticket", ticketRouter);
app.use("/api/secondary", secondaryRouter);

if (process.env.NODE_ENV === "production") {
    app.use(express.static(path.join(__dirname, "./build")));

    app.get("/*", function (req, res) {
        res.sendFile(path.join(__dirname + "/build/index.html"));
    });
}

const PORT = process.env.NODE_ENV === "production" ? 8080 : 3001;

mongoose.set('useFindAndModify', false);
mongoose.connect(mongoUrl, {
    useNewUrlParser: true,
    useUnifiedTopology: true
}, function (err, res) {
    if (err) {
        console.log('ERROR connecting to: ' + mongoUrl + '. ' + err);
    } else {
        console.log('Succeeded connected to: ' + mongoUrl);
    }
});

app.listen(PORT, () => console.log(`Server listening on port ${PORT}!`));
