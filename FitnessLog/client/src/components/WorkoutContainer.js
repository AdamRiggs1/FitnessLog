import React, { useState, useEffect } from "react";
import UserStrengthWorkout from "./UserStrengthWorkout";
import UserCardioWorkout from "./UserCardioWorkout";

export const WorkoutContainer = () => {
    const userStrengthWorkoutUrl = '/api/UserStrengthWorkout/'
    const userCardioWorkoutUrl = '/api/UserCardioWorkout/'
    const [userStrengthWorkouts, setUserStrengthWorkouts] = useState([])
    const [userCardioWorkouts, setUserCardioWorkouts] = useState([])

    useEffect(() => {
        fetch(userStrengthWorkoutUrl).then(res => (res.json())).then(workouts => setUserStrengthWorkouts(workouts))

    }
        , []
    )

    useEffect(() => {
        fetch(userCardioWorkoutUrl).then(res => (res.json())).then(workouts => setUserCardioWorkouts(workouts))
    }
        , []
    )

    return (<div>
        <div>
            {userStrengthWorkouts.map((userStrengthWorkout) => (<UserStrengthWorkout userStrengthWorkout={userStrengthWorkout} key={userStrengthWorkout.id} />))}
        </div>
        <div>
            {userCardioWorkouts.map((userCardioWorkout) => (<UserCardioWorkout userCardioWorkout={userCardioWorkout} key={userCardioWorkout.id} />))}
        </div>
    </div>
    )
}

export default WorkoutContainer