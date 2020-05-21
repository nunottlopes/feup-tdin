var express = require("express");
var router = express.Router();
const Ticket = require("../models/ticket");
const Secondary = require("../models/secondary");

router.get("/:id", (req, res) => {
    Ticket.findById(req.params.id, (err, result) => {
        if (err) {
            res.status(404).send();
        } else if (result) {
            res.status(200).send(result);
        }
    });
});

router.get("/", (req, res) => {
    Ticket.find(req.query, (err, result) => {
        if (err) {
            res.status(500).send();
        } else if (result) {
            res.status(200).send(result);
        }
    });
});

router.post("/", (req, res) => {
    var t = new Ticket({
        name: req.body.name,
        email: req.body.email,
        title: req.body.title,
        description: req.body.description
    });

    // Saving it to the database.
    t.save((err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(201).send({
                id: result._id
            });
        }
    });

});

router.put("/:id", (req, res) => {
    Ticket.findByIdAndUpdate(req.params.id, req.body, (err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(200).send(result);
        }
    });
});

router.put("/:id/solve", (req, res) => {
    const update = {
        status: "solved",
        response: req.body.response
    };

    Ticket.findByIdAndUpdate(req.params.id, update, (err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(200).send(result);
        }
    });

    //TODO: Send email
});

router.put("/:id/assign", (req, res) => {

    const query = {
        _id: req.params.id,
        status: "unassigned"
    }

    const update = {
        status: "assigned",
        solver: req.body.solver
    };

    Ticket.findOneAndUpdate(query, update, (err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(200).send(result);
        }
    });

});

router.get("/:id/secondary", (req, res) => {
    Secondary.find({ original: req.params.id }, (err, result) => {
        if (err) {
            res.status(500).send();
        } else if (result) {
            res.status(200).send(result);
        }
    });
});

module.exports = router;
