const express = require("express");
var path = require("path");
var cors = require("cors");

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

app.use("/api/", indexRouter);

if (process.env.NODE_ENV === "production") {
    app.use(express.static(path.join(__dirname, "./build")));

    app.get("/*", function (req, res) {
        res.sendFile(path.join(__dirname + "/build/index.html"));
    });
}

const PORT = process.env.NODE_ENV === "production" ? 8080 : 3001;

app.listen(PORT, () => console.log(`Server NUNO listening on port ${PORT}!`));
