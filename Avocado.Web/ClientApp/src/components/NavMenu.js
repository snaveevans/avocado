import React from "react";
import { Link } from "react-router-dom";
import "./NavMenu.css";

export default props => (
  <div>
    <Link to="/">Home</Link>
    <Link to="/events">Events</Link>
    <Link to="/login">Login/Register</Link>
  </div>
);
