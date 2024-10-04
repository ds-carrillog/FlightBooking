import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../styles/Statistics.css';  // Import CSS for styling statistics

const Statistics = () => {
  const [topAirlines, setTopAirlines] = useState([]); // State to hold top airlines data
  const [airlinesCount, setAirlinesCount] = useState(0); // State to hold total airline count

  // Fetch statistics data when component mounts
  useEffect(() => {
    fetchStatistics(); // Call the function to fetch both airline count and top airlines
  }, []);

  // Function to fetch both total airlines count and top airlines data
  const fetchStatistics = () => {
    // Fetch the total number of airlines
    axios.get(`${process.env.REACT_APP_API_URL}/Flights/Statistics/AirlinesCount`)
      .then(response => setAirlinesCount(response.data))
      .catch(error => console.error('Error fetching airline count:', error));

    // Fetch the top 3 airlines with the most reservations
    axios.get(`${process.env.REACT_APP_API_URL}/Flights/Statistics/TopAirlines`)
  .then(response => setTopAirlines(response.data.slice(0, 3)))  // Limit to top 3 airlines
  .catch(error => {
    console.error('Error fetching top airlines:', error);  // Log any errors
    console.log(process.env.REACT_APP_API_URL); // Debería imprimir la URL del backend
  });

  };

  return (
    <div className="statistics">
      <h4 className="chart-title">Top 3 Airlines by Number of Reservations</h4>  {/* Chart title */}

      {/* Vertical Bar Graph to display top 3 airlines by reservations */}
      <div className="vertical-bar-graph">
        {topAirlines.map((airline, index) => (
          <div key={index} className="vertical-bar-container">
            {/* Vertical bar with dynamic height based on reservations count */}
            <div 
              className={`vertical-bar color-${index + 1}`} 
              style={{ height: `${airline.reservationsCount * 30}px` }}  // Adjust height dynamically
            >
              <span>{airline.reservationsCount}</span>  {/* Show reservation count inside the bar */}
            </div>
            <span className="label">{airline.airline}</span>  {/* Airline label below the bar */}
          </div>
        ))}
      </div>

      {/* Display total number of registered airlines */}
      <h4>Total Number of Registered Airlines: {airlinesCount}</h4>
    </div>
  );
};

export default Statistics;
