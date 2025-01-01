import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../components/controlledform.css";

const LoginForm = () => {
  const [formData, setFormData] = useState({ email: "", password: "" });
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };
  const Validate = () => {
    let formErrors = {};

    if (!formData.email) {
      formErrors.email = "Email Required.";
    }

    if (!formData.password) {
      formErrors.password = "Please enter the Password";
    }

    if (Object.keys(formErrors).length > 0) {
      setErrors(formErrors);
      return false;
    }

    return true;
  };
  const handleSubmit = (e) => {
    console.log("handleSubmit function called");
    e.preventDefault();

    const isValid = Validate();
    console.log("Validation result:", isValid);

    if (isValid) {
      navigate("/sample");
      console.log("Form submitted successfully with:", formData);
    } else {
      console.log("Form errors:", errors);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Email</label>
        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
        />
        {errors.email && <span style={{ color: "red" }}>{errors.email}</span>}
      </div>
      <div>
        <label>Password</label>
        <input
          type="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
        />
        {errors.password && (
          <span style={{ color: "red" }}>{errors.password}</span>
        )}
      </div>
      <button type="submit">Login</button>
    </form>
  );
};

export default LoginForm;