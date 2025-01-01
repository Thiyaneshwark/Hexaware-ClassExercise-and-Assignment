import React from "react";
import { useDispatch } from "react-redux";
import { toggleTodo } from "./store";

function Todo({ todo, index }) {
  const dispatch = useDispatch();

  return (
    <li
      className="list-group-item"
      style={{ textDecoration: todo.completed ? "line-through" : "none" }}
      onClick={() => dispatch(toggleTodo(index))}
    >
      {todo.text}
    </li>
  );
}

export default Todo;