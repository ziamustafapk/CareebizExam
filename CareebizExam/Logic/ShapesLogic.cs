using System;
using System.Collections.Generic;
using System.Transactions;
using AutoMapper;
using CareebizExam.DTO;
using CareebizExam.Infrastructure;
using CareebizExam.Model;

namespace CareebizExam.Logic
{
    public class ShapesLogic :IShapesLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShapesLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<ShapesDTO> GetAllShapes()
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<Shapes>, IEnumerable<ShapesDTO>>
                        ( _unitOfWork.Shapes.GetAll());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public IEnumerable<ShapesDTO> GetShapesByIds(int[] ids)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<Shapes>, IEnumerable<ShapesDTO>>
                        (_unitOfWork.Shapes.GetShapesByIds(ids));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public ShapesDTO GetShapeById(int id)
        {
            try
            {
                return
                    _mapper.Map<Shapes, ShapesDTO>
                        (_unitOfWork.Shapes.Get(id));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public ShapesDTO AddShape(ShapesRequest request)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var shape = new Shapes
                        {
                        Title = request.Title,
                            Area = request.Area,
                            Description = request.Description,
                            Latitude = request.Latitude,
                            Longitude = request.Longitude,
                            ShapeType = request.ShapeType,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                    };
                    _unitOfWork.Shapes.Add(shape);
                    _unitOfWork.SaveChanges();
                    scope.Complete();
                    return
                        _mapper.Map<Shapes, ShapesDTO>
                            (shape);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        
    }

    public interface IShapesLogic
    {
        #region CustomMethods
        IEnumerable<ShapesDTO> GetAllShapes();
        IEnumerable<ShapesDTO> GetShapesByIds(int[] ids);
        ShapesDTO GetShapeById(int id);
        ShapesDTO AddShape(ShapesRequest request);
        

        #endregion CustomMethods
    }
}
