import React from "react";
const users=[
    {id:1,name:"Ram",city:"Chennai",age:21},
    {id:2,name:"Jaanu",city:"Chennai",age:22},
    {id:3,name:"sita",city:"Mumbai",age:20},
    {id:4,name:"Karthik",city:"Kolkata",age:23},
];

export default function UserList(){
    return (
        <>
        <div id="firstDiv">UserList Forst Div</div>
        <div>
            <table border="1px">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Age</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map((user)=>(
                        <tr key={user.id}>
                            <td>{user.id}</td>
                            <td>{user.name}</td>
                            <td>{user.age}</td>

                        </tr>
                    ))}
                </tbody>

            </table>
        </div>
        </>
    );
}