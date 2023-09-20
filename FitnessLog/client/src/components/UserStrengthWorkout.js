import { useState, useEffect } from "react"
import { getAllStrengthWorkouts } from "../modules/userStrengthWorkoutManager";
import { UserStrengthWorkoutProperties } from "./UserStrengthWorkoutProperties";

export const UserStrengthWorkout = () => {

    //create a userStrengthWorkout state
    const [userStrengthWorkouts, setUserStrengthWorkouts] = useState([])

    //insert a useEffect to invoke the getAllStrenthWorkout function, then insert the userStrengthWorkouts state
    useEffect(() => {
        getAllStrengthWorkouts().then(setUserStrengthWorkouts)
    }, []);

    //create a jsx file iterating through the different strength workouts
    return (
        <>
            <div>
                {userStrengthWorkouts.map((userStrengthWorkout) => (<UserStrengthWorkoutProperties userStrengthWorkout={userStrengthWorkout} key={userStrengthWorkout.id} />))}
            </div>
        </>
    );

}

export default UserStrengthWorkout
