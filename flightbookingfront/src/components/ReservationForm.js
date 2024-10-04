import React, { useState } from 'react';
import '../styles/ReservationForm.css'; // Import CSS for reservation form styling

const ReservationForm = ({ flight, onSubmit }) => {
  const [passengerName, setPassengerName] = useState(''); // State to track the input passenger name

  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();

    const now = new Date();
    const departureDate = new Date(flight.departureDate);

    // Validate: Check if the flight's departure date has passed
    if (departureDate < now) {
        alert('You cannot reserve a flight that has already departed.');
        return;
    }

    // If validation passes, proceed with the API call
    onSubmit(passengerName);
  };

  return (
    <div className="reservation-form">
      <h3>Reserve Flight to {flight.destination}</h3> {/* Display flight destination */}
      <form onSubmit={handleSubmit}>
        <input 
          name="passengerName" 
          placeholder="Passenger Name" 
          value={passengerName} 
          onChange={(e) => setPassengerName(e.target.value)} // Update state on input change
          required 
        />
        <button type="submit">Confirm Reservation</button> {/* Submit button */}
      </form>
    </div>
  );
};

export default ReservationForm;
