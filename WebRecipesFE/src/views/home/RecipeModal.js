import React from 'react'
import { CButton } from '@coreui/react'
import './RecipeModal.css' // Styles for text and image only
import { useNavigate } from 'react-router-dom'
import axios from 'axios'
import AllRecipeComments from './Comments/AllRecipeComments'

const RecipeModal = ({ recipe }) => {
  const token = localStorage.getItem('token')
  const navigate = useNavigate() // React Router navigation hook

  const EditOrderPage = () => {
    navigate(`/edit-recipe/${recipe.id}`) // Navigate without reload
  }

  const DeleteRecipe = () => {
    axios
      .delete(`https://localhost:7120/api/Recipe/${recipe.id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        alert('Recipe deleted successfully!')
        window.location.reload()
      })
      .catch((error) => {
        console.error('Error deleting recipe:', error)
        alert('Failed to delete recipe.')
      })
  }
  return (
    <>
      <div className="image-wrapper">
        <img src={recipe.pictureUrl} alt={recipe.pavadinimas} className="recipe-image" />
      </div>

      <h2 className="recipe-title">{recipe.pavadinimas}</h2>
      <p className="recipe-description">{recipe.tekstas}</p>

      <h3 className="instructions-title">Gaminimo instrukcija:</h3>
      <p className="recipe-instructions">{recipe.instrukcija}</p>

      <div className="button-container">
        <CButton color="primary" className="me-2" onClick={EditOrderPage}>
          Edit
        </CButton>
        <CButton color="danger" onClick={DeleteRecipe}>
          Delete
        </CButton>
      </div>
      <h3>Komentarai</h3>
      <AllRecipeComments recipeId={recipe.id} />
    </>
  )
}

export default RecipeModal
