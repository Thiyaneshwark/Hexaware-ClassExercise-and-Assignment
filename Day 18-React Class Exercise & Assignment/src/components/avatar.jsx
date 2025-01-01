import React from "react";
import img1 from "../assets/Image1.jpg";
import img2 from "../assets/Image2.jpg";
import img3 from "../assets/Image3.jpg";

function Avatar({ person, size }) {
  return (
    <>
      <p>{person.name}</p>
      <img 
        src={person.imageId} 
        alt={person.name} 
        height={size} 
        width={size} 
      />
    </>
  );
}

export default function Profile() {
  return (
    <>
      <Avatar person={{ name: "Person 1", imageId: img1 }} size={150} />
      <Avatar person={{ name: "Person 2", imageId: img2 }} size={150} />
      <Avatar person={{ name: "Person 3", imageId: img3 }} size={150} />
    </>
  );
}

export { Avatar };
