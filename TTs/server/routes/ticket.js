var express = require("express");
var router = express.Router();
const Ticket = require("../models/ticket");
const Secondary = require("../models/secondary");
const nodemailer = require("nodemailer");

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
            req.io.emit('create', null);
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

    Ticket.findByIdAndUpdate(req.params.id, update, { new: true }, (err, result) => {
        if (err) {
            res.status(500).send(err);
        } else {
            res.status(200).send(result);
            sendEmail(result);
        }
    });
});

const sendEmail = (ticket) => {
    console.log("Sending email to " + ticket.email);
    const transporter = nodemailer.createTransport({
        service: "gmail",
        auth: {
            user: "tdintts@gmail.com",
            pass: "tdin2020"
        }
    });

    const mailOptions = {
        from: "tdin-tts@gmail.com",
        to: ticket.email,
        subject: "The ticket you submitted has been solved!",
        html: `<h2>Your ticket:</h2> \
        <h4>Title:</h4>${ticket.title} \
        <h4>Description:</h4>${ticket.description} \
        <h4>Submitted at:</h4>${ticket.createdAt} \
        <hr></hr> \
        <h3>Response:</h3>${ticket.response} \
        <h4>Was solved by ${ticket.solver}</h4>`
    };

    transporter.sendMail(mailOptions, (error, info) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Email sent: " + info.response);
        }
    });

}

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
            req.io.emit('assign', req.body.solver);
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
