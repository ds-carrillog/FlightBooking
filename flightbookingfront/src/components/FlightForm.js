import React, { useState } from 'react';
import axios from 'axios';
import '../styles/FlightForm.css'; // Import CSS for styling the form

const FlightForm = ({ onFlightAdded }) => {
  // State to manage the input data for the form
  const [flightData, setFlightData] = useState({
    origin: '',
    destination: '',
    departureDate: '',
    arrivalDate: '',
    airline: '',
    price: ''
  });

  // Handle changes for all form inputs
  const handleChange = (e) => {
    const { name, value } = e.target;
    // Update the state for the field that changed
    setFlightData({ ...flightData, [name]: value });
  };

  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();

    const now = new Date();
    const departureDate = new Date(flightData.departureDate);
    const arrivalDate = new Date(flightData.arrivalDate);

    // Validate: Departure date should not be in the past
    if (departureDate < now) {
        alert('Departure date cannot be in the past.');
        return;
    }

    // Validate: Departure date should be before the arrival date
    if (departureDate > arrivalDate) {
        alert('Departure date cannot be after the arrival date.');
        return;
    }

    // If validation passes, proceed with the API call
    axios.post(`${process.env.REACT_APP_API_URL}/Flights`, flightData) // Use the environment variable
        .then((response) => {
            onFlightAdded(response.data);
            setFlightData({
                origin: '',
                destination: '',
                departureDate: '',
                arrivalDate: '',
                airline: '',
                price: ''
            });
        })
        .catch((error) => console.error('Error adding flight:', error));
  };

  return (
    <form className="flight-form" onSubmit={handleSubmit}>
      {/* Form inputs with state management */}
      <input name="origin" placeholder="Origin" value={flightData.origin} onChange={handleChange} required />
      <input name="destination" placeholder="Destination" value={flightData.destination} onChange={handleChange} required />
      <input name="departureDate" placeholder="Departure Date" type="datetime-local" value={flightData.departureDate} onChange={handleChange} required />
      <input name="arrivalDate" placeholder="Arrival Date" type="datetime-local" value={flightData.arrivalDate} onChange={handleChange} required />
      <input name="airline" placeholder="Airline" value={flightData.airline} onChange={handleChange} required />
      <input name="price" placeholder="Price" type="number" value={flightData.price} onChange={handleChange} required />
      <button type="submit">Add Flight</button> {/* Submit button */}
    </form>
  );
};

export default FlightForm;
