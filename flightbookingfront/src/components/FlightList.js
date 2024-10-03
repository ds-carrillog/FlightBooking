import React, { useState } from 'react';
import axios from 'axios';
import ReservationForm from './ReservationForm';
import '../styles/FlightList.css'; // Import CSS for flight list styling

const FlightList = ({ flights, onReservation, refreshStatistics }) => {
  const [selectedFlight, setSelectedFlight] = useState(null); // Track the selected flight
  const [showModal, setShowModal] = useState(false); // Manage modal visibility

  // Handle the 'Reserve' button click by selecting the flight and showing the modal
  const handleReserveClick = (flight) => {
    setSelectedFlight(flight);
    setShowModal(true);
  };

  // Handle reservation submission
  const handleReservation = (passengerName) => {
    axios.post('http://localhost:8080/Reservations', { flightId: selectedFlight.id, passengerName })
      .then(() => {
        // Reset the state and close the modal after a successful reservation
        setSelectedFlight(null);
        setShowModal(false);
        onReservation();  // Notify parent to refresh the flight list
        refreshStatistics();  // Also trigger stats update
      })
      .catch(error => console.error('Error making reservation:', error)); // Log errors
  };

  // If no flights are available, display a message
  if (!flights || flights.length === 0) {
    return (
      <div style={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center'
      }}>
        No flights available.
      </div>
    );
  }

  return (
    <div className="flight-list">
      <table>
        <thead>
          <tr>
            <th>Origin</th>
            <th>Destination</th>
            <th>Departure</th>
            <th>Arrival</th>
            <th>Airline</th>
            <th>Price</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {flights.map(flight => (
            <tr key={flight.id}>
              <td>{flight.origin}</td>
              <td>{flight.destination}</td>
              <td>{new Date(flight.departureDate).toLocaleString()}</td>
              <td>{new Date(flight.arrivalDate).toLocaleString()}</td>
              <td>{flight.airline}</td>
              <td>${flight.price.toFixed(2)}</td>
              <td>
                {/* Button to reserve a flight */}
                <button onClick={() => handleReserveClick(flight)}>Reserve</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Show the Reservation form modal when a flight is selected */}
      {showModal && (
        <ReservationForm 
          flight={selectedFlight} 
          onSubmit={handleReservation} 
          onClose={() => setShowModal(false)} // Close modal on cancel
        />
      )}
    </div>
  );
};

export default FlightList;
