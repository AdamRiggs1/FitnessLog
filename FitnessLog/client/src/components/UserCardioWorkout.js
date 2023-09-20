import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react"
import { getAllUserCardioWorkouts } from "../modules/userCardioWorkoutManager";
import { UserCardioWorkoutProperties } from "./UserCardioWorkoutProperties";

export const UserCardioWorkout = () => {

    //create a state userCardioWorkouts
    const [userCardioWorkouts, setUserCardioWorkouts] = useState([])


    //create a useEffect to import the getAlluserCardioWorkouts function, then set a paramter for the userCardioWorkoutState
    useEffect(() => {
        getAllUserCardioWorkouts().then(setUserCardioWorkouts)
    }, []);

    //create a jsx file iterating through the cardio workouts
    return (
        <>
            <div>
                {userCardioWorkouts.map((userCardioWorkout) => (<UserCardioWorkoutProperties userCardioWorkout={userCardioWorkout} key={userCardioWorkout.id} />))}
            </div>
        </>
    );

}

export default UserCardioWorkout