class AccessPoint {
    static getEndPoint() {
        const devServer = "http://localhost:3001/api";
        const prodServer = "http://localhost:3001/api";
        return process.env.NODE_ENV === "production" ? prodServer : devServer;
    }
}

export default AccessPoint;
