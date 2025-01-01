import logo from "./logo.svg";
import "./App.css";
import Header from "./components/Header";
import Home from "./components/home";
import Products from "./components/Product";
import Footer from "./components/footer";
import Login from "./components/Login";
import HomePage from "./components/HomePage";
import { useState } from "react";
import { AuthProvider } from "./components/AuthContext";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute";
function App() {
  return (
    <div className="App">
      <AuthProvider>
        <Router>
          <Header />
          <div className="content">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/login" element={<Login />} />
              <Route element={<ProtectedRoute />}>
                <Route path="/products" element={<Products />} />
              </Route>
            </Routes>
          </div>
          <Footer />
        </Router>
      </AuthProvider>
    </div>
  );
}
export default App;