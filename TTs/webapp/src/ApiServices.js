import axios from "axios";
import AccessPoint from "./config/AccessPoint";

export const sendTicket = async (data) => {
    return await axios.post(`${AccessPoint.getEndPoint()}/ticket`, data);
};
