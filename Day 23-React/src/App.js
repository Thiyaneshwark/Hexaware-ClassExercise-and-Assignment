import logo from "./logo.svg";
import "./App.css";
import AddTodo from "./AddTodo";
import TodoList from "./TodoList";
import { useState } from "react";
import store from "./store";
import { Provider } from "react-redux";

function App() {
  // const [todos, setTodos] = useState([]);

  // const addTodo = (todo) => {
  //   setTodos([...todos, todo]);
  // };

  // const toggleComplete = (index) => {
  //   const newTodos = todos.map((todo, idx) => {
  //     if (idx === index) {
  //       return { ...todo, completed: !todo.completed };
  //     }
  //     return todo;
  //   });
  //   setTodos(newTodos);
  // };

  return (
    <Provider store={store}>
      <div>
        <h1>Todo List</h1>
        <AddTodo />
        <TodoList />
      </div>
    </Provider>
  );
}

export default App;