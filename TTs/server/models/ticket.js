const mongoose = require("mongoose");

let TicketSchema = new mongoose.Schema({
    name: { type: String, required: true },
    email: { type: String, required: true },
    title: { type: String, required: true },
    description: { type: String, required: true },
    solver: { type: String, required: false },
    response: { type: String, required: false },
    status: {
        type: String,
        enum: ["unassigned", "assigned", "waiting", "solved", "answered"],
        default: "unassigned"
    }
}, {
    timestamps: true
});


// Export the model
module.exports = mongoose.model("Ticket", TicketSchema);
