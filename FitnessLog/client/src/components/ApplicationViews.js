import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import WorkoutContainer from "./WorkoutContainer";
import UserStrengthWorkout from "./UserStrengthWorkout";
import UserCardioWorkout from "./UserCardioWorkout";
import EditCardioWorkoutForm from "./EditCardioWorkoutForm";
import EditStrengthWorkoutForm from "./EditStrengthWorkoutForm";

export default function ApplicationViews({ isLoggedIn, profile }) {
    return (
        <main>
            <Routes>
                <Route path="/">
                    <Route
                        index
                        element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="hello"
                        element={isLoggedIn ? < Hello /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="workoutList"
                        element={isLoggedIn ? < WorkoutContainer /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="editCardio/:cardioWorkoutId"
                        element={isLoggedIn ? < EditCardioWorkoutForm /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="editStrength/:strengthWorkoutId"
                        element={isLoggedIn ? < EditStrengthWorkoutForm /> : <Navigate to="/login" />}
                    />

                    <Route path="login" element={<Login />} />
                    <Route path="register" element={<Register />} />
                    <Route path="*" element={<p>Whoops, nothing here...</p>} />
                </Route>
            </Routes>
        </main>
    );
};