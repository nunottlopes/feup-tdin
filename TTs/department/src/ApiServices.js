import axios from "axios";

// DATABASE SERVICES

const API_URL = "http://localhost:3001/api"

const solveSecondayQuestion = async (question_id, response) => {
  const body = {
    response: response,
  }

  return await axios.put(`${API_URL}/secondary/${question_id}/solve`, body);
}

export const ApiServices = {
  solveSecondayQuestion
};
