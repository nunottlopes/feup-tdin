import axios from "axios";

// DATABASE SERVICES

const API_URL = "http://localhost:3001/api"

const getUnassignedTickets = async () => {
  return await axios.get(`${API_URL}/ticket?status=unassigned`);
};

const getMyTickets = async (name) => {
  const requests = [
    axios.get(`${API_URL}/ticket?status=answered&solver=${name}`),
    axios.get(`${API_URL}/ticket?status=assigned&solver=${name}`),
    axios.get(`${API_URL}/ticket?status=waiting&solver=${name}`)
  ]

  return await Promise.all(requests);
}

const getSecondayTickets = async (ticket_id, solver) => {
  return await axios.get(`${API_URL}/secondary?original=${ticket_id}&solver=${solver}`);
}

const assignTicket = async (solver, ticket_id) => {
  const body = {
    solver: solver,
  }

  return await axios.put(`${API_URL}/ticket/${ticket_id}/assign`, body)
}

const solveTicket = async (response, ticket_id) => {
  const body = {
    response: response,
  }

  return await axios.put(`${API_URL}/ticket/${ticket_id}/solve`, body)
}

const createSecondaryTicket = async (ticket, department, question) => {
  const body = {
    original: ticket._id,
    question: question,
    solver: ticket.solver,
    department: department
  }

  return await axios.post(`${API_URL}/secondary`, body)
}

export const ApiServices = {
  createSecondaryTicket,
  solveTicket,
  getUnassignedTickets,
  getMyTickets,
  getSecondayTickets,
  assignTicket
};
