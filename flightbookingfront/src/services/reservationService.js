// src/services/reservationService.js
import axios from 'axios';

// Base URL for the API
const API_URL = 'http://localhost:8080';

// Make a reservation by sending flight ID and passenger name to the API
export const makeReservation = (flightId, passengerName) =>
  axios.post(`${API_URL}/Reservations`, { flightId, passengerName });
