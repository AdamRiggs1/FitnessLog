import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import UserStrengthWorkout from "./UserStrengthWorkout";
import UserCardioWorkout from "./UserCardioWorkout";

export const WorkoutContainer = () => {

    const StrengthWorkout = UserStrengthWorkout();
    const CardioWorkout = UserCardioWorkout();

    const navigate = useNavigate();

    return (<div>
        <h4>Strength Workouts</h4>
        <div>
            {StrengthWorkout}
        </div>
        <h4>Cardio Workouts</h4>
        <div>
            {CardioWorkout}
        </div>
        <div>
            <button onClick={() => navigate()}>Add Workout</button>
        </div>
    </div>
    )
}

export default WorkoutContainer