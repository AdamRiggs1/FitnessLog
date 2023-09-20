import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { updateFood, getFoodbyId } from "../modules/foodManager";


export const EditFoodForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */
    //create a variable to use the parameter for the particular Id of the given food
    const { foodId } = useParams()
    //invoke the navigate varialbe 
    const navigate = useNavigate()

    //set the state for the food variable with the appropriate keys
    const [food, update] = useState({
        name: "",
        calories: 0,
        carbohydrates: 0,
        protein: 0,
        fat: 2,
        id: foodId
    })

    //bring in the useEffect
    //invoke the getFoodyById from the manager and insert into it as a parameter the specific foodId 
    useEffect(
        () => {
            getFoodbyId(foodId).then(
                (food) => {
                    update(food)
                }
            )
        },
        []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        // TODO: Create the object to be saved to the API

        //invoke an updateFood function and insert the state into it
        return updateFood(food)
            .then(
                //after the state is updated, navigate back to the food list
                () => {
                    navigate(`/foodList`)
                }
            )
    }


    //create a jsx to make a form to edit the existing information
    return (<>
        <form className="editFoodForm">
            <h2 className="editFoodForm__title">Edit Food</h2>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="name">Name:</label>
                    <input
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="name of food"
                        value={food.name}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.name = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="food__calories"> Amount of Calories:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="number of calories"
                        value={food.calories}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.calories = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="carbohydrates">Carbohydrates:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="carbs"
                        value={food.carbohydrates}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.carbohydrates = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="protein">Protein:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="protein"
                        value={food.protein}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.protein = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>


            <fieldset>
                <div className="form-group">
                    <label htmlFor="fat">Fat:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="fat"
                        value={food.fat}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.fat = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <section className="food_edit_buttons">
                <button
                    onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Submit Changes
                </button>
                <button className="food_edit_back" onClick={() => { navigate("/foodList") }}>Cancel Edit</button>
            </section>
        </form>
    </>
    )


}

export default EditFoodForm