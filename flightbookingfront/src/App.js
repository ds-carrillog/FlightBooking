import React from 'react';
import './App.css';  // Import global styles
import Home from './pages/Home';  // Import the Home component, which manages FlightList, FlightForm, etc.

function App() {
  return (
    <div className="App">
      {/* Render the Home component that includes flight-related features */}
      <Home /> 
    </div>
  );
}

export default App;
