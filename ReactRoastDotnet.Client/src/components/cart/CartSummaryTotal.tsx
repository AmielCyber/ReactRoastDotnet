type Props = {
   totalPrice: string;
   totalItems: number;
}
function CartSummaryTotal(props: Props){
   return(
       <section className="flex flex-row justify-between w-full">
           <div className="card-body flex flex-col">
               <p className="text-lg">Total Items:</p>
               <p className="card-title text-lg">Total Price:</p>
           </div>
           <div className="card-body flex flex-col ml-auto items-end">
               <strong className="text-secondary">{props.totalItems}</strong>
               <strong className="text-emerald-500">{props.totalPrice}</strong>
           </div>
       </section>
   )
}
export default CartSummaryTotal;
