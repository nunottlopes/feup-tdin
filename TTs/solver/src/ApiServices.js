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

// const getPatternData = async (paper_id, pattern_id) => {
//   return await axios.get(`${AccessPoint.getDatabaseEndPoint()}/paper/${paper_id}/pattern/${pattern_id}`);
// };

// const updatePaper = async(paper_id, values) => {
//   const data = {
//     title: values.title,
//     conference: values.conference,
//     date: values.date,
//     authors: values.authors,
//     keywords: values.keywords
//   }

//   return await axios.put(`${AccessPoint.getDatabaseEndPoint()}/paper/${paper_id}`, data);
// };


export const ApiServices = {
  getUnassignedTickets,
  getMyTickets,
  getSecondayTickets
};
