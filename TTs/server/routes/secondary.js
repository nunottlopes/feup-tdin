var express = require("express");
var router = express.Router();
const AmqpManager = require("../amqp/AmqpManager");
const Secondary = require("../models/secondary")

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

router.post("/", async (req, res) => {
    const broker = await AmqpManager.getInstance();
    await broker.send(req.body.department, Buffer.from(JSON.stringify(req.body)));

    var t = new Secondary({
        original: req.body.original,
        question: req.body.question,
        solver: req.body.solver,
        department: req.body.department
    });

    // Saving it to the database.
    t.save((err, result) => {
        if (err) {
            console.log(err)
            res.status(500).send(err);
        } else {
            res.status(201).send({
                id: result._id
            });
        }
    });

});

// TODO: /:id/solve
// para depois enviar mensagem para solver

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
