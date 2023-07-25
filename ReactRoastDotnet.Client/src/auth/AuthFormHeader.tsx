import CoffeeIcon from "../icons/CoffeeIcon.tsx";

type Props = {
    title: string;
}

function AuthFormHeader(props: Props) {
    return (
        <div className="flex flex-col justify-center items-center text-center lg:text-left">
            <div className="mt-10 text-primary">
                <CoffeeIcon/>
            </div>
            <h1 className="mt-2 text-center text-2xl font-bold leading-9 text-transparent bg-clip-text bg-gradient-to-r from-primary to-secondary">
                {props.title}
            </h1>
        </div>
    )
}

export default AuthFormHeader;
