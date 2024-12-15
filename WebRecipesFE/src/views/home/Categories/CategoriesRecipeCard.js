import React, { useEffect, useState } from 'react'
import {
  CCard,
  CCardBody,
  CCardImage,
  CCardTitle,
  CCol,
  CModal,
  CModalHeader,
  CModalBody,
} from '@coreui/react'
import './RecipeCard.css'
import RecipeModal from './RecipeModal'

const CategoriesRecipeCard = ({ recipe }) => {
  const [isModalVisible, setIsModalVisible] = useState(false)

  const closeModal = () => {
    setIsModalVisible(false)
  }
  const openModal = () => {
    setIsModalVisible(true)
  }
  return (
    <CCol xs={12} sm={6} md={4} lg={3} className="mb-4">
      <CCard
        className="ccard"
        onClick={() => openModal()}
        style={{ cursor: 'pointer', height: '100%' }}
      >
        <div className="image-container">
          <CCardImage
            orientation="top"
            src={recipe.pictureUrl}
            alt={recipe.pavadinimas}
            className="card-image"
          />
        </div>
        <CCardBody className="d-flex align-items-center justify-content-center">
          <CCardTitle className="text-center">{recipe.pavadinimas}</CCardTitle>
        </CCardBody>
        <CModal backdrop={'static'} size="xl" visible={isModalVisible} onClose={closeModal}>
          <CModalHeader onClose={closeModal}>{recipe.pavadinimas}</CModalHeader>
          <CModalBody>
            <RecipeModal recipe={recipe} />
          </CModalBody>
        </CModal>
      </CCard>
    </CCol>
  )
}
export default CategoriesRecipeCard
