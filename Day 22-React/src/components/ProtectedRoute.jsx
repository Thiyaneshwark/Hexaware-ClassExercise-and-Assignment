import { Navigate, Outlet } from "react-router-dom";
import { AuthContext } from "./AuthContext";
import { useCallback, useContext } from "react";

export default function ProtectedRoute({ condition }) {
  const { isLoggedIn } = useContext(AuthContext);

  const isAllowed = isLoggedIn && (condition ? condition() : true);
  return isAllowed ? <Outlet /> : <Navigate to="/login" />;
}