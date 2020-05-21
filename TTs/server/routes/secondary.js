var express = require("express");
var router = express.Router();
const AmqpManager = require("../amqp/AmqpManager");
const Secondary = require("../models/secondary");
const Ticket = require("../models/ticket");

router.get("/:id", (req, res) => {
    Secondary.findById(req.params.id, (err, result) => {
        if (err) {
            res.status(404).send();
        } else if (result) {
            res.status(200).send(result);
        }
    });
});

router.get("/", (req, res) => {
    Secondary.find(req.query, (err, result) => {
        if (err) {
            res.status(500).send();
        } else if (result) {
            res.status(200).send(result);
        }
    });
});

router.post("/", (req, res) => {
    var t = new Secondary({
        original: req.body.original,
        question: req.body.question,
        solver: req.body.solver,
        department: req.body.department
    });

    // Saving it to the database.
    t.save((err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(201).send({
                id: result._id
            });
            sendToQueue(result);
        }
    });

    Ticket.findByIdAndUpdate(req.body.original, { status: "waiting" }, (err, result) => {
        if (err) {
            console.log(err);
        } else {
            console.log(result._id + " status updated to WAITING");
        }
    });

});

const sendToQueue = async (secondary) => {
    const originalTicket = await Ticket.findById(secondary.original);

    secondary.original = originalTicket;

    const broker = await AmqpManager.getInstance();
    await broker.send(secondary.department, Buffer.from(JSON.stringify(secondary)));
}

router.put("/:id/solve", (req, res) => {
    const update = {
        status: "solved",
        response: req.body.response
    };

    Secondary.findByIdAndUpdate(req.params.id, update, async (err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(200).send(result);
            if (result && await isTicketSolved(result.original)) {
                answeredOriginal(result.original)
            }
        }
    });

    //TODO: Notify solver
});

const isTicketSolved = async (id) => {
    let tickets = await Secondary.find({ original: id });
    return tickets.every(e => e.status === "solved");
}

const answeredOriginal = (id) => {
    Ticket.findByIdAndUpdate(id, { status: "answered" }, (err, result) => {
        if (err) {
            console.log(err);
        } else {
            console.log(result._id + " status updated to ANSWERED");
        }
    });
}

router.put("/:id", (req, res) => {
    Secondary.findByIdAndUpdate(req.params.id, req.body, (err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(200).send(result);
        }
    });
});

module.exports = router;
