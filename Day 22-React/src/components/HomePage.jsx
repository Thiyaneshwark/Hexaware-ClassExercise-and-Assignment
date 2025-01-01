import React, { useContext, useState } from "react";
import { AuthContext } from "./AuthContext";
const HomePage = () => {
  const { isAuthenticated, login } = useContext(AuthContext);
  return (
    <div>
      <h1> Home Page </h1>
      {isAuthenticated ? (
        <h3>Welcome,User!...</h3>
      ) : (
        <button onClick={login}>Login</button>
      )}
    </div>
  );
};

export default HomePage;