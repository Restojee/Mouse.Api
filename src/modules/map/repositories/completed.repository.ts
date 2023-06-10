import {
    Inject,
    Injectable
} from "@nestjs/common";
import { Repository } from "sequelize-typescript";
import { CompletedEntity } from "../../../database/entities/completed.entity";
import {
    CreateCompletedArgs,
    GetCompletedByUserAndMap,
    UpdateCompletedArgs
} from "../map.types";

@Injectable()
export class CompletedRepository {
    constructor(
        @Inject('COMPLETED_REPOSITORY')
        private repository: Repository<CompletedEntity>
    ) {}
    async updateCompleted(updateOneCompletedRequest: UpdateCompletedArgs): Promise<void> {
        const { image, completedId } = updateOneCompletedRequest;
        await this.repository.update({ image }, { where: { id: completedId } })
    }

    async createCompleted(createOneCompletedRequest: CreateCompletedArgs): Promise<CompletedEntity> {
        const { userId, mapId, image } = createOneCompletedRequest;
        return await this.repository.create({ userId, mapId, image });
    }

    async getCompletedByUserAndMap(getCompletedByUserAndMapArgs: GetCompletedByUserAndMap): Promise<CompletedEntity> {
        const { userId, mapId } = getCompletedByUserAndMapArgs;
        return await this.repository.findOne({ where: { userId, mapId } })
    }
}
