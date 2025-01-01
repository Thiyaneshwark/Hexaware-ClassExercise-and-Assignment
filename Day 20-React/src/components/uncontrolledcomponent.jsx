import React, { useRef, useState } from "react";
import "./controlledform.css";
import { useNavigate } from "react-router-dom";

const UnControlledLoginForm = () => {
  const emailRef = useRef();
  const passwordRef = useRef();
  const [errors, setErros] = useState({});
  const navigate = useNavigate();
  const handleSubmit = (e) => {
    e.preventDefault();
    const email = emailRef.current.value;
    const password = passwordRef.current.value;
    console.log("handlesubmit called");
    let valid = true;
    let errors = {};
    if (!email) {
      valid = false;
      errors.email = "Enter the email";
    }
    if (!password) {
      valid = false;
      errors.password = "Enter the Password";
    }
    console.log("Before valid if contion " + valid);
    if (valid) {
      console.log("Login Data", email, password);
      setErros({});
      navigate("/sample");
    } else {
      console.log("Erros", errors);
      setErros(errors);
    }
  };

  return (
    <>
      <h1> Uncontrolled Component Login Form demo</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Email</label>
          <input type="email" ref={emailRef} />
          {errors.email && <span style={{ color: "red" }}>{errors.email}</span>}
        </div>
        <div>
          <label>Password</label>
          <input type="password" ref={passwordRef} />
          {errors.password && (
            <span style={{ color: "red" }}>{errors.password}</span>
          )}
        </div>
        <button type="submit">Login</button>
      </form>
    </>
  );
};

export default UnControlledLoginForm;