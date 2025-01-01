import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { addTodo } from "./store";

function AddTodo() {
  const [value, setValue] = useState("");
  const dispatch = useDispatch();

  const handleSubmit = (e) => {
    e.preventDefault();
    if (value) {
      console.log("Dispatching addTodo:", value);
      dispatch(addTodo(value));
      setValue("");
    }
  };

  return (
    <div className="container">
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <input
            type="text"
            className="form-control"
            value={value}
            onChange={(e) => setValue(e.target.value)}
          />
        </div>
        <button type="submit" className="btn btn-success">
          Add Todo
        </button>
      </form>
    </div>
  );
}

export default AddTodo;