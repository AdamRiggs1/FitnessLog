import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { addStrengthWorkout } from "../modules/strengthWorkoutManager";
import { addUserStrengthWorkout } from "../modules/userStrengthWorkoutManager";
import { me } from "../modules/authManager";


export const AddStrengthWorkoutForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */

    //invoke the navigate function
    const navigate = useNavigate()

    //set the state for strengthWorkout with the appropriate keys
    const [strengthWorkout, setStrengthWorkout] = useState({
        name: "",
        reps: 0,
        sets: 0,
        weight: 0,
        typeId: 1,
    })




    const handleSaveButtonClick = (event) => {
        event.preventDefault()


        // TODO: Create the object to be saved to the API

        //invoke the addStrengthWorkout function function and insert the strengthWorkout state
        return addStrengthWorkout(strengthWorkout)
            .then((strengthWorkoutObject) => {
                //then invoke the imported me function to get the currentn user 
                me()
                    //then put the userProfileObject as the parameter, make a new state for the userSTrengthWorkout with userRfileId and strengthWorkoutId key
                    .then((userProfileObject) => {
                        const userStrengthWorkout = {
                            userProfileId: userProfileObject.id,
                            strengthWorkoutId: strengthWorkoutObject.id
                        }
                        addUserStrengthWorkout(userStrengthWorkout)

                    })
                    //then navigate back to the workout list
                    .then(
                        () => {
                            navigate(`/workoutList`)
                        }
                    )
            })
    }



    return (<>
        <form className="addStrengthWorkoutForm">
            <h2 className="addStrengthWorkoutForm__title">Add Strength Workout</h2>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="name">Name:</label>
                    <input
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="name of strength workout"
                        value={strengthWorkout.name}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.name = evt.target.value
                                setStrengthWorkout(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="weight"> Amount of Weight:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="amount of weight"
                        value={strengthWorkout.weight}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.weight = evt.target.value
                                setStrengthWorkout(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="address">Amount of Reps:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="reps"
                        value={strengthWorkout.reps}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.reps = evt.target.value
                                setStrengthWorkout(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="address">Amount of Sets:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="sets"
                        value={strengthWorkout.sets}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.sets = evt.target.value
                                setStrengthWorkout(copy)
                            }
                        } />
                </div>
            </fieldset>

            <section className="strengthWorkout_add_buttons">
                <button
                    onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Submit Changes
                </button>
                <button className="strength_add_back" onClick={() => { navigate("/workoutList") }}>Cancel Add Workout</button>
            </section>
        </form>
    </>
    )


}

export default AddStrengthWorkoutForm