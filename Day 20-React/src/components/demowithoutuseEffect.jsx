import React, { useEffect, useState } from "react";
const DepartmentList = () => {
  const [departments, SetDepartments] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchDepartments = async () => {
      const response = await fetch("https://localhost:7253/api/Departments");
      const data = await response.json();
      SetDepartments(data);
      setLoading(false);
    };
    fetchDepartments();
  }, []);

  return (
    <>
      <div>
        {loading ? (
          <p>Loading ...</p>
        ) : (
          <ul>
            {departments.map((department) => (
              <li key={department.id}>{department.name}</li>
            ))}
          </ul>
        )}
      </div>
    </>
  );
};

export default DepartmentList;