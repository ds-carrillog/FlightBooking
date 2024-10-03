import React, { useState } from 'react';
import axios from 'axios';
import '../styles/SearchBar.css'; // Import CSS for search bar styling

const SearchBar = ({ onSearchResults }) => {
  // State to manage the search input data
  const [searchData, setSearchData] = useState({
    origin: '',
    destination: '',
    startDate: '',
    endDate: ''
  });

  // Handle changes in the input fields
  const handleChange = (e) => {
    const { name, value } = e.target;
    setSearchData({ ...searchData, [name]: value }); // Update the specific field in the state
  };

  // Handle the search button click to fetch flight data
  const handleSearch = () => {
    axios.get(`http://localhost:8080/Flights/Search`, { params: searchData }) // Pass search parameters
      .then(response => onSearchResults(response.data)) // Pass results to parent component
      .catch(error => console.error('Error searching flights:', error)); // Log any errors
  };

  return (
    <div className="search-bar">
      {/* Search input fields */}
      <input name="origin" placeholder="Origin" value={searchData.origin} onChange={handleChange} />
      <input name="destination" placeholder="Destination" value={searchData.destination} onChange={handleChange} />
      <input name="startDate" placeholder="Start Date" type="date" value={searchData.startDate} onChange={handleChange} />
      <input name="endDate" placeholder="End Date" type="date" value={searchData.endDate} onChange={handleChange} />
      <button onClick={handleSearch}>Search</button> {/* Search button */}
    </div>
  );
};

export default SearchBar;
