import React from "react";
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

export default function Products() {
  return (
    <div className="container">
      <h2>Product List</h2>
      <table className="table table-bordered">
        <thead>
          <tr>
            <td>Id</td>
            <td>Name</td>
            <td>description</td>
            <td>IsDelivered</td>
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
                  <span className="text-success">&#10004;</span>
                ) : (
                  <span className="text-danger"> Pending</span>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}