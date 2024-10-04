import React, { useState, useEffect } from 'react';
import FlightForm from '../components/FlightForm';
import FlightList from '../components/FlightList';
import SearchBar from '../components/SearchBar';
import Statistics from '../components/Statistics';
import axios from 'axios';
import '../styles/Header.css';  // Import the CSS file for the header

const Home = () => {
  const [flights, setFlights] = useState([]);  // State to store the list of flights
  const [refreshFlag, setRefreshFlag] = useState(false);  // Flag to trigger statistics refresh

  // Fetch the list of flights when the component mounts or refreshFlag changes
  useEffect(() => {
    fetchFlights();
  }, []);

  // Function to fetch flights from the API
  const fetchFlights = () => {
    axios.get(`${process.env.REACT_APP_API_URL}/Flights`)  // Use the environment variable
      .then(response => setFlights(response.data))  // Update the flights state
      .catch(error => console.error('Error fetching flights:', error));  // Handle any errors
  };

  // Function to trigger statistics refresh
  const refreshStatistics = () => {
    setRefreshFlag(prevFlag => !prevFlag);  // Toggle the refresh flag to re-fetch statistics
  };

  // Handle the addition of a new flight and update the flight list
  const handleFlightAdded = (newFlight) => {
    setFlights([...flights, newFlight]);  // Add the new flight to the flights state
    refreshStatistics();  // Refresh statistics after a new flight is added
  };

  // Handle search results and update the flight list
  const handleSearchResults = (searchResults) => {
    setFlights(searchResults);  // Update the flights state with the search results
  };

  // Handle refresh after a reservation is made (only for statistics)
  const handleReservationMade = () => {
    refreshStatistics();  // Refresh only the statistics after a reservation
  };

  return (
    <div>
      {/* Header Section */}
      <header className="main-header">
        <h1>Flight Booking</h1>  {/* Main title */}
      </header>

      {/* Sections for Search, Flight List, Add Flight, and Statistics */}
      
      {/* Search Section */}
      <section id="search" className="section">
        <h3>Search Flights</h3>
        <SearchBar onSearchResults={handleSearchResults} />  {/* Search bar component */}
      </section>

      {/* Flights List Section */}
      <section id="flights" className="section">
        <h3>Available Flights</h3>
        <FlightList flights={flights} onReservation={handleReservationMade} refreshStatistics={refreshStatistics} />  {/* Flight list component */}
      </section>

      {/* Add New Flight Section */}
      <section id="add-flight" className="section">
        <h3>Add a New Flight</h3>
        <FlightForm onFlightAdded={handleFlightAdded} />  {/* Form to add a new flight */}
      </section>

      {/* Statistics Section */}
      <section id="statistics" className="section">
        <h3>Flight Statistics</h3>
        <Statistics key={refreshFlag} />  {/* Re-render statistics when refreshFlag changes */}
      </section>
    </div>
  );
};

export default Home;
