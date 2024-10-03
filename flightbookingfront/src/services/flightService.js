// src/services/flightService.js
import axios from 'axios';

// Base URL for the API
const API_URL = 'http://localhost:8080';

// Fetch all flights from the API
export const getFlights = () => axios.get(`${API_URL}/Flights`);

// Add a new flight by sending a POST request with flight data
export const addFlight = (flight) => axios.post(`${API_URL}/Flights`, flight);

// Search for flights by passing search parameters (origin, destination, date range)
export const searchFlights = (searchParams) => axios.get(`${API_URL}/Flights/Search`, { params: searchParams });

// Get the top airlines based on the number of reservations
export const getTopAirlines = () => axios.get(`${API_URL}/Flights/Statistics/TopAirlines`);

// Get the total number of registered airlines
export const getAirlinesCount = () => axios.get(`${API_URL}/Flights/Statistics/AirlinesCount`);
