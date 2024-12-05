// import React, { useState } from 'react';
// import { useNavigate } from 'react-router-dom';
// import { toast, ToastContainer } from 'react-toastify';
// import 'react-toastify/dist/ReactToastify.css';
// import axios from 'axios';
// import {jwtDecode} from 'jwt-decode';
// import Cookies from 'js-cookie';
// import './Signin.css';

// const Signin = () => {
//   const [email, setEmail] = useState('');
//   const [password, setPassword] = useState('');
//   const [userName, setUserName] = useState('');
//   const [gender, setGender] = useState('');
//   const [dept, setDept] = useState('');
//   const [designation, setDesignation] = useState('');
//   const [phoneNumber, setPhoneNumber] = useState('');
//   const [address, setAddress] = useState('');
//   const [branch, setBranch] = useState('');
//   const [userType, setUserType] = useState('Employee');
//   const [isSignUp, setIsSignUp] = useState(false);
//   const [loading, setLoading] = useState(false);
//   const [passwordVisible, setPasswordVisible] = useState(false);
//   const navigate = useNavigate();

//   const handleLogin = async (e) => {
//     e.preventDefault();
//     setLoading(true);

//     const loginData = { UserName: userName, Password: password };

//     try {
//       const response = await axios.post('https://localhost:7144/api/Auth/login', loginData);
//       const { token } = response.data;

//       if (!token) throw new Error('No token received');

//       Cookies.set('token', token, { expires: 7, path: '' });

//       const decoded = jwtDecode(token);
//       console.log('Decoded token:', decoded);

//       toast.success('Login Successful!', { autoClose: 2000 });

//       if (decoded.role === 'Admin') {
//         navigate('/admin/Dashboard', { replace: true });
//       } else if (decoded.role === 'Employee') {
//         navigate('/EmpDashboard', { replace: true });
//       } else {
//         toast.error('Unknown user role', { autoClose: 2000 });
//       }
//     } catch (err) {
//       const errorMessage = err.response?.data?.message || err.message || 'Invalid credentials';
//       toast.error("Invalid credentials, Please Enter the correct credentials", { autoClose: 2000 });
//     } finally {
//       setLoading(false);
//     }
//   };

//   const handleRegister = async (e) => {
//     e.preventDefault();

//     const registerData = {
//       UserName: userName,
//       UserMail: email,
//       Gender: gender,
//       Dept: dept,
//       Designation: designation,
//       PhoneNumber: phoneNumber,
//       Address: address,
//       Password: password,
//       Branch: branch,
//       UserType: userType,
//     };

//     const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%?&])[A-Za-z\d@$!%?&]{8,}$/;
//     if (!passwordRegex.test(password)) {
//       toast.error(
//         'Password must be at least 8 characters long, contain one uppercase letter, one lowercase letter, one number, and one special character.',
//         { autoClose: 2000 }
//       );
//       return;
//     }

//     if (!/^\S+@\S+\.\S+$/.test(email)) {
//       toast.error('Invalid email format.', { autoClose: 2000 });
//       return;
//     }

//     try {
//       await axios.post('https://localhost:7144/api/Users', registerData);
//       toast.success('Registration Successful!', { autoClose: 2000 });
//       setTimeout(() => navigate('/', { replace: true }), 3000);
//     } catch (err) {
//       const errorMessage = err.response?.data?.message || 'Registration failed';
//       toast.error(errorMessage, { autoClose: 2000 });
//     }
//   };

//   const styles = {
//     greeting: {
//       marginBottom: '20px',
//       textAlign: 'center',
//       color: '#444',
//     }
//   };

//   return (
//     <div className="signin-container">
//       <div style={styles.greeting}>
//         <h2>Welcome to Asset Management</h2>
//         <p>Your one-stop solution for tracking and maintaining your assets efficiently.</p>
//         <p>Manage logs, monitor costs, and ensure optimal performance of your assets with ease.</p>
//       </div>

//       <div className="signin-form-container">
//         <div className="text-center">
//           <h2 className="title">
//             {isSignUp ? 'Create an Account' : 'Sign in to Hexa Asset Management'}
//           </h2>
//         </div>

//         <form onSubmit={isSignUp ? handleRegister : handleLogin} className="signin-form">
//           <input
//             type="text"
//             placeholder="Username"
//             value={userName}
//             onChange={(e) => setUserName(e.target.value)}
//             className="form-input"
//             required
//           />
//           <div className="password-container">
//             <input
//               type={passwordVisible ? 'text' : 'password'} // Toggle password visibility
//               placeholder="Password"
//               value={password}
//               onChange={(e) => setPassword(e.target.value)}
//               className="password-input"
//               required
//             />
//             <span
//               className="eye-icon"
//               onClick={() => setPasswordVisible(!passwordVisible)} // Toggle state
//             >
//               {passwordVisible ? '👁️' : '👁️‍🗨️'} {/* Icons for show/hide */}
//             </span>
//           </div>
//           {isSignUp && (
//             <>
//               <input
//                 type="email"
//                 placeholder="Email"
//                 value={email}
//                 onChange={(e) => setEmail(e.target.value)}
//                 className="form-input"
//                 required
//               />
//               <input
//                 type="text"
//                 placeholder="Gender"
//                 value={gender}
//                 onChange={(e) => setGender(e.target.value)}
//                 className="form-input"
//                 required
//               />
//               <input
//                 type="text"
//                 placeholder="Department"
//                 value={dept}
//                 onChange={(e) => setDept(e.target.value)}
//                 className="form-input"
//                 required
//               />
//               <input
//                 type="text"
//                 placeholder="Designation"
//                 value={designation}
//                 onChange={(e) => setDesignation(e.target.value)}
//                 className="form-input"
//                 required
//               />
//               <input
//                 type="text"
//                 placeholder="Phone Number"
//                 value={phoneNumber}
//                 onChange={(e) => setPhoneNumber(e.target.value)}
//                 className="form-input"
//                 required
//               />
//               <input
//                 type="text"
//                 placeholder="Address"
//                 value={address}
//                 onChange={(e) => setAddress(e.target.value)}
//                 className="form-input"
//                 required
//               />
//               <input
//                 type="text"
//                 placeholder="Branch"
//                 value={branch}
//                 onChange={(e) => setBranch(e.target.value)}
//                 className="form-input"
//                 required
//               />
//               <select
//                 value={userType}
//                 onChange={(e) => setUserType(e.target.value)}
//                 className="form-input"
//                 required
//               >
//                 <option value="Employee">Employee</option>
//                 <option value="Admin">Admin</option>
//               </select>
//             </>
//           )}
//           <button
//             type="submit"
//             className="submit-button"
//             disabled={loading}
//           >
//             {loading ? 'Processing...' : isSignUp ? 'Sign Up' : 'Sign In'}
//           </button>
//           {!isSignUp && (
//             <button
//               type="button"
//               onClick={() => setIsSignUp(true)}
//               className="register-button"
//             >
//               Register
//             </button>
//           )}
//         </form>

//         <div className="text-center mt-4">
//           <button
//             type="button"
//             onClick={() => setIsSignUp(!isSignUp)}
//             className="switch-form-button"
//           >
//             {isSignUp ? 'Already have an account? Sign in' : 'New here? Create an account'}
//           </button>
//         </div>
//       </div>

//       <ToastContainer />
//     </div>
//   );
// };

// export default Signin;


import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import {jwtDecode} from 'jwt-decode';
import Cookies from 'js-cookie';
import './Signin.css';

const Signin = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userName, setUserName] = useState('');
  const [gender, setGender] = useState('');
  const [dept, setDept] = useState('');
  const [designation, setDesignation] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [address, setAddress] = useState('');
  const [branch, setBranch] = useState('');
  const [userType, setUserType] = useState('Employee');
  const [isSignUp, setIsSignUp] = useState(false);
  const [loading, setLoading] = useState(false);
  const [passwordVisible, setPasswordVisible] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setLoading(true);

    const loginData = { UserName: userName, Password: password };

    try {
      const response = await axios.post('https://localhost:7144/api/Auth/login', loginData);
      const { token } = response.data;

      if (!token) throw new Error('No token received');

      Cookies.set('token', token, { expires: 7, path: '' });

      const decoded = jwtDecode(token);
      console.log('Decoded token:', decoded);

      toast.success('Login Successful!', { autoClose: 2000 });

      if (decoded.role === 'Admin') {
        navigate('/admin/Dashboard', { replace: true });
      } else if (decoded.role === 'Employee') {
        navigate('/EmpDashboard', { replace: true });
      } else {
        toast.error('Unknown user role', { autoClose: 2000 });
      }
    } catch (err) {
      const errorMessage = err.response?.data?.message || err.message || 'Invalid credentials';
      toast.error("Invalid credentials, Please Enter the correct credentials", { autoClose: 2000 });
    } finally {
      setLoading(false);
    }
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    const registerData = {
      UserName: userName,
      UserMail: email,
      Gender: gender,
      Dept: dept,
      Designation: designation,
      PhoneNumber: phoneNumber,
      Address: address,
      Password: password,
      Branch: branch,
      UserType: userType,
    };

    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%?&])[A-Za-z\d@$!%?&]{8,}$/;
    if (!passwordRegex.test(password)) {
      toast.error(
        'Password must be at least 8 characters long, contain one uppercase letter, one lowercase letter, one number, and one special character.',
        { autoClose: 2000 }
      );
      return;
    }

    if (!/^\S+@\S+\.\S+$/.test(email)) {
      toast.error('Invalid email format.', { autoClose: 2000 });
      return;
    }

    try {
      await axios.post('https://localhost:7144/api/Users', registerData);
      toast.success('Registration Successful!', { autoClose: 2000 });
      setTimeout(() => navigate('/', { replace: true }), 3000);
    } catch (err) {
      const errorMessage = err.response?.data?.message || 'Registration failed';
      toast.error(errorMessage, { autoClose: 2000 });
    }
  };

  const styles = {
    greeting: {
      marginBottom: '20px',
      textAlign: '',
      color: '#444',
    }
  };
  return (
    <div className="signin-container">
      {/* <div style={styles.greeting}>
        <h2>Welcome to Asset Management</h2>
        <p>Your one-stop solution for tracking and maintaining your assets efficiently.</p>
        <p>Manage logs, monitor costs, and ensure optimal performance of your assets with ease.</p>
      </div> */}

      <div className="signin-form-container">
        <div className="text-center">
          <h2 className="title">
            {isSignUp ? 'Create an Account' : 'Sign in to Hexa Asset Management'}
          </h2>
        </div>

        <form onSubmit={isSignUp ? handleRegister : handleLogin} className="signin-form">
          <input
            type="text"
            placeholder="Username"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            className="form-input"
            required
          />
          <div className="password-container">
            <input
              type={passwordVisible ? 'text' : 'password'}
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="password-input"
              required
            />
            <span
              className="eye-icon"
              onClick={() => setPasswordVisible(!passwordVisible)}
            >
              {passwordVisible ? '👁️' : '👁️‍🗨️'}
            </span>
          </div>
          {isSignUp && (
            <>
              <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="form-input"
                required
              />
              <input
                type="text"
                placeholder="Gender"
                value={gender}
                onChange={(e) => setGender(e.target.value)}
                className="form-input"
                required
              />
              <input
                type="text"
                placeholder="Department"
                value={dept}
                onChange={(e) => setDept(e.target.value)}
                className="form-input"
                required
              />
              <input
                type="text"
                placeholder="Designation"
                value={designation}
                onChange={(e) => setDesignation(e.target.value)}
                className="form-input"
                required
              />
              <input
                type="text"
                placeholder="Phone Number"
                value={phoneNumber}
                onChange={(e) => setPhoneNumber(e.target.value)}
                className="form-input"
                required
              />
              <input
                type="text"
                placeholder="Address"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
                className="form-input"
                required
              />
              <input
                type="text"
                placeholder="Branch"
                value={branch}
                onChange={(e) => setBranch(e.target.value)}
                className="form-input"
                required
              />
              <select
                value={userType}
                onChange={(e) => setUserType(e.target.value)}
                className="form-input"
                required
              >
                <option value="Employee">Employee</option>
                <option value="Admin">Admin</option>
              </select>
            </>
          )}
          <button
            type="submit"
            className="submit-button"
            disabled={loading}
          >
            {loading ? 'Processing...' : isSignUp ? 'Sign Up' : 'Sign In'}
          </button>
          {!isSignUp && (
            <button
              type="button"
              onClick={() => setIsSignUp(true)}
              className="register-button"
            >
              Register
            </button>
          )}
        </form>

        <div className="text-center mt-4">
          <button
            type="button"
            onClick={() => setIsSignUp(!isSignUp)}
            className="switch-form-button"
          >
            {isSignUp ? 'Already have an account? Sign in' : 'New here? Create an account'}
          </button>
        </div>
      </div>

      <ToastContainer />
    </div>
  );
};

export default Signin;

