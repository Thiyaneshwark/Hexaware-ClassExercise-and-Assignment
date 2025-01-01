import React from "react";
import "./ProductList.css";

const products = [
  {
    id: 1,
    name: "Laptop",
    description: "A high-performance laptop",
    isDelivered: true,
  },
  {
    id: 2,
    name: "Phone",
    description: "A smartphone with a large display",
    isDelivered: false,
  },
  {
    id: 3,
    name: "Tablet",
    description: "A tablet with a sleek design",
    isDelivered: true,
  },
  { id: 4, name: "Monitor", description: "A 4K monitor", isDelivered: false },
  {
    id: 5,
    name: "Keyboard",
    description: "A mechanical keyboard",
    isDelivered: true,
  },
];

function ProductList() {
  return (
    <div>
      <h1>Product List</h1>
      <table border="1">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Description</th>
            <th>Delivered</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td>{product.description}</td>
              <td>
                {product.isDelivered ? (
                  <span className="delivered">&#10004;</span>
                ) : (
                  "Pending"
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ProductList;
