import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import AddCardioWorkoutForm from "./AddCardioWorkoutForm"
import AddStrengthWorkoutForm from "./AddStrengthWorkoutForm";
import { getAllWorkoutTypes } from "../modules/workoutTypeManager";

export const AddWorkoutFormContainer = () => {

    //set the states for workout types, cardio, and strength workouts
    const [workoutTypes, setWorkoutTypes] = useState([])
    //for cardio and strength workouts, set the default state to "false"
    const [cardioWorkoutType, setCardioWorkoutType] = useState(false)
    const [strengthWorkoutType, setStrengthWorkoutType] = useState(false)



    useEffect(() => {
        getAllWorkoutTypes()
            .then((wt) => {
                setWorkoutTypes(wt)
            })
    }, [])

    //const strengthWorkoutForm = AddStrengthWorkoutForm();
    //const cardioWorkoutForm = AddCardioWorkoutForm();



    //create a jsx file where when a number for a corresponding workout type is chosen, a specifc form is rendered conditionally
    //if one is selected, the setStrengthWorkoutType is true, so the add strength workout form is rendered. if 2 is selected, then the setCArdioWorkoutType is true so that the cardio form is rendered. then create an else statement to where they are both false
    return (<div>
        <h4>Choose Workout Type</h4>

        <label htmlFor="workout_type">Workout Type:</label>
        <select
            required
            className="workout_type"
            onChange={
                (evt) => {
                    if (evt.target.value === "1") {
                        setStrengthWorkoutType(true)
                        setCardioWorkoutType(false)
                    }
                    else if (evt.target.value === "2") {
                        setStrengthWorkoutType(false)
                        setCardioWorkoutType(true)
                    }
                    else {
                        setStrengthWorkoutType(false)
                        setCardioWorkoutType(false)
                    }
                }
            } >
            <option value={0}>Choose Workout Type</option>
            {
                workoutTypes.map(
                    wt => {
                        return <option key={wt.id} value={wt.id}>{wt.type}</option>
                    }

                )
            }
        </select>

        {strengthWorkoutType && <AddStrengthWorkoutForm />}
        {cardioWorkoutType && <AddCardioWorkoutForm />}
        {!strengthWorkoutType && !cardioWorkoutType && <div>
            "select option to make form appear"</div>}
    </div>
    )
}

export default AddWorkoutFormContainer