import React from "react";
import { useForm,useFieldArray } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import "./controlledform.css";

const RegistrationFormWithDynamicFields=()=>{
    const{
        register,
        control,
        handleSubmit,
        formState:{errors},
    }=useForm();

    const {fields,append,remove}=useFieldArray({
        control,
        name:"addresses",
    });

    const navigate=useNavigate();

    const onSubmit=(data)=>{
        console.log("Registered Details are", data);
        navigate("/sample");
    };
    return(
        <>
        <h1>Registration Form</h1>
        <form onSubmit={handleSubmit(onSubmit)}>
            <div>
                <lable>Name</lable>
                <input
                type="text"
                id="name"
                {...register("name",{required:"Name Field Required"})}
                />
                {errors.name &&(
                    <spam style={{color:"red"}}>{errors.message}</spam>
                )}
            </div>
            <div>
            <label>Email</label>
          <input
            type="text"
            id="email"
            {...register("email", { required: "Email Field Required" })}
          />
          {errors.email && (
            <span style={{ color: "red" }}>{errors.email.message}</span>
          )}
        </div>
        <div>
          <label>Password</label>
          <input
            type="text"
            id="password"
            {...register("password", { required: "Password Field Required" })}
          />
          {errors.password && (
            <span style={{ color: "red" }}>{errors.password.message}</span>
          )}
        </div>
        <h3> Address </h3>
        {fields.map((item, index) => (
          <div key={item.id}>
            <label>Address {index + 1} </label>
            <input
              type="text"
              {...register(`addresses.${index}.line1`, {
                required: "addresses is required.",
              })}
            />
            {errors.addresses?.[index]?.line1 && (
              <span style={{ color: "red" }}>
                {errors.addresses[index].line1.message}
              </span>
            )}
            <button type="button" onClick={() => remove(index)}>
              {" "}
              Remove Addresss
            </button>
          </div>
        ))}
        <button type="button" onClick={() => append({ line1: "" })}>
          Add Addresss
        </button>
        <button type="submit">Register</button>
      </form>
    </>
  );
};

export default RegistrationFormWithDynamicFields;