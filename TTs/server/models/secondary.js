const mongoose = require("mongoose");

let SecondarySchema = new mongoose.Schema({
    original: { type: mongoose.Schema.Types.ObjectId, ref: 'Ticket', required: true },
    title: { type: String, required: true },
    description: { type: String, required: true },
    solver: { type: String, required: true },
    department: { type: String, required: true },
    response: { type: String, required: false },
    status: {
        type: String,
        enum: ["unsolved", "solved"],
        default: "unsolved"
    }
}, {
    timestamps: true
});


// Export the model
module.exports = mongoose.model("Secondary", SecondarySchema);
