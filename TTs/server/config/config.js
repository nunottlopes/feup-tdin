module.exports = {
    mongodb: {
        url: process.env.MONGODB_URL,
        dbName: process.env.MONGODB_DATABASE_NAME,
        config: { useUnifiedTopology: true }
    }
};