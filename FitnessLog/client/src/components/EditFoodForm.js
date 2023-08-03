import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { updateFood, getFoodbyId } from "../modules/foodManager";


export const EditFoodForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */
    const { foodId } = useParams()
    const navigate = useNavigate()

    const [food, update] = useState({
        name: "",
        calories: 0,
        carbohydrates: 0,
        protein: 0,
        fat: 2,
        id: foodId
    })
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

        return updateFood(food)
            .then(
                () => {
                    navigate(`/foodList`)
                }
            )
    }



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