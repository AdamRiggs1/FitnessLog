import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react"
import { getAllStrengthWorkouts } from "../modules/userStrengthWorkoutManager";
import { UserStrengthWorkoutProperties } from "./UserStrengthWorkoutProperties";

export const UserStrengthWorkout = () => {

    const [userStrengthWorkouts, setUserStrengthWorkouts] = useState([])

    const navigate = useNavigate()



    useEffect(() => {
        getAllStrengthWorkouts().then(setUserStrengthWorkouts)
    }, []);

    return (
        <>
            <div>
                {userStrengthWorkouts.map((userStrengthWorkout) => (<UserStrengthWorkoutProperties userStrengthWorkout={userStrengthWorkout} key={userStrengthWorkout.id} />))}
            </div>
        </>
    );

}

export default UserStrengthWorkout
