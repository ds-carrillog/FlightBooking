// src/services/reservationService.js
import axios from 'axios';

// Base URL for the API
const API_URL = process.env.REACT_APP_API_URL; // Use the environment variable

// Make a reservation by sending flight ID and passenger name to the API
export const makeReservation = (flightId, passengerName) =>
  axios.post(`${API_URL}/Reservations`, { flightId, passengerName });
