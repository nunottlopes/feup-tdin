var express = require("express");
var router = express.Router();
const Ticket = require("../models/ticket")

/* GET home page. */
router.get("/", (req, res) => {
    res.send("TDIN");
});

module.exports = router;
